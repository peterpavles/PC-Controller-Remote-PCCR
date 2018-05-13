<?php
class CommandsController extends Controller {
	
    function gets() {
        $this->isAuthMachine();
        $r = array();

        $this->db->delete('commands', array('time <=' => time() - 1800));

        $commands = $this->db->select()
        ->where('pc_id', $this->machine->id)
        ->get('commands')
        ->result();

        foreach ($commands as $k => $v) {
            $this->db->delete('commands', array('id' => $v->id));
        }

        $r['commands'] = $commands;
        $r['pc_id'] = $this->machine->id;
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->set($r);
    }

    function get($command_id = null) {
        $this->isAuthMachine();
        $r = array();

        $commands = $this->db->select()
        ->where('id', $command_id)
        ->get('commands')
        ->result();

        $r['commands'] = $commands;
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->set($r);
    }

    function delete($id = null) {
        $this->isAuthMachine();
        $r = array('success' => true, 'message' => 'Deleted');
        if(is_numeric($id))
            $this->db->delete('commands', array('id' => $id));
        else
            $r =array('success' => FALSE, 'message' => 'Id not numeric');
    }

    /***************************** Admin **********************************/

    function post() {
        $this->isAuthAdmin();
        $this->Validator->verifyRequiredParams(array('pc_id', 'priority', 'content'), true);
        $r = array();

        $pc_id = $this->posted('pc_id');
        $priority = $this->posted('priority');
        $content = $this->posted('content');

        $this->db->insert('commands', array(
            'admin_id' => $this->admin->id,
            'pc_id' => $pc_id,
            'priority' => $priority,
            'content' => $content
        ));

        $r['success'] = true;
        $r['message'] = 'OK';

        $this->status_code = 201;
        $this->set($r);
    }

}