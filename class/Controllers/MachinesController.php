<?php
class MachinesController extends Controller {
	
    function post() {
        $r = array();
        $this->Validator->verifyRequiredParams(array('guid', 'name', 'os', 'ram', 'processor'), true);

        $guid = $this->posted('guid');
        $name = $this->posted('name');
        $os = $this->posted('os');
        $ram = $this->posted('ram');
        $processor = $this->posted('processor');

        $api_key = $this->genRandStr();
        $refresh_time = 1500;

        $machine_found = $this->db->select()
        ->where('guid', $guid)
        ->get('pcs')
        ->row();

        if (empty($machine_found)) {
            $this->db->insert('pcs', array(
                'guid' => $guid,
                'name' => $name,
                'os' => $os,
                'ram' => $ram,
                'processor' => $processor,
                'api_key' => $api_key,
                'refresh_time' => $refresh_time,
                'ip' => $this->getIP(),
                'country' => $this->getCountry(),
                'country_code' => $this->getCountryCode(),
                'last_time' => time(),
                'date_created' => date('Y-m-d H:i:s')
            ));
            $this->status_code = 200;
        } else {
            $this->db->where('id', $machine_found->id)->update('pcs', array(
                'api_key' => $api_key,
                'ip' => $this->getIP(),
                'last_time' => time(),
            ));
            $refresh_time = $machine_found->refresh_time;
            $this->status_code = 201;
        }

        $r['api_key'] = $api_key;
        $r['refresh_time'] = $refresh_time;
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->set($r);
    }

    /***************************** Admin **********************************/

    
    function gets() {
        $this->isAuthAdmin();

        $machines = $this->db->select('id,guid,name,os,ip,country,country_code,ram,processor,last_time,refresh_time,date_created')
        ->order_by('date_created', 'DESC')
        ->get('pcs')
        ->result();

        $r['machines'] = $machines;
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->set($r);
    }

    function get($id = null) {
        $this->isAuthAdmin();

        $machine = $this->db->select('id,guid,name,os,ip,country,country_code,ram,processor,last_time,refresh_time,date_created')
        ->where('id', $id)
        ->get('pcs')
        ->row();

        $r['machine'] = $machine;
        $r['success'] = true;
        $r['message'] = 'OK';
        
        $this->set($r);
    }

    function delete($pc_id = null) {
        $this->isAuthAdmin();
        $this->db->delete('pcs', array('id' => $pc_id));
        $r['success'] = true;
        $r['message'] = 'OK';
        $this->set($r);
    }

}