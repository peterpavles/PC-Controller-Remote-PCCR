<?php
class ResultsController extends Controller {
	
    function post() {
        $this->isAuthMachine();
        $this->Validator->verifyRequiredParams(array('admin_id', 'pc_id', 'content'), true);
        $r = array();

        $admin_id = $this->posted('admin_id');
        $pc_id = $this->posted('pc_id');
        $content = $this->posted('content');

        $this->db->insert('results', array(
            'admin_id' => $admin_id,
            'pc_id' => $this->machine->id,
            'content' => $content
        ));

        $r['success'] = true;
        $r['message'] = 'OK';

        $this->status_code = 201;
        $this->set($r);
    }

    function put($id) {
        $this->isAuthMachine();
        $this->Validator->verifyRequiredParams(array('admin_id', 'pc_id', 'content'));
        $r = array();

        $admin_id = $this->posted('admin_id');
        $pc_id = $this->posted('pc_id');
        $content = $this->posted('content');

        $this->db->where('id', $id)->update(array(
            'admin_id' => $admin_id,
            'pc_id' => $this->machine->id,
            'content' => $content
        ));
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->status_code = 200;
        $this->set($r);
    }

    /***************************** Admin **********************************/

    function get($pc_id = null) {
        $this->isAuthAdmin();

        $results = $this->db->select()
        ->where('admin_id', $this->admin->id)
        ->where('pc_id', $pc_id)
        ->get('results')
        ->result();
        
        $pc = $this->db->select('id,name,last_time')
        ->where('id', $pc_id)
        ->get('pcs')
        ->row(); 

        foreach ($results as $k => $v) {
            $this->db->delete('results', array('id' => $v->id));
        }
        
        $r['results'] = $results;
        $r['pc'] = $pc;
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->set($r);
    }

    
    function gets() {
        $this->isAuthAdmin();

        $results = $this->db->select()
        ->where('admin_id', $this->admin->id)
        ->get('results')
        ->results();
        
        $r['results'] = $results;
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->set($r);
    }

    function delete($id = null) {
        $this->isAuthAdmin();
        $this->db->delete('results', array('id' => $id));
        $r['success'] = true;
        $r['message'] = 'OK';
        $this->set($r);
    }

}