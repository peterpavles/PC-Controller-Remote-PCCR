<?php
class AdminController extends Controller {
	
	function index() {
		$this->set(array(
            'test' => true, 
            'fake' => true, 
            'api_key' => $this->genRandStr()
        ));
	}

	function login() {
        $r = array();
        Controller::$encrypt = false;
		$this->Validator->verifyRequiredParams(array('email', 'password'), true);

		$email = $this->app->request->post('email');
        $password = $this->app->request->post('password');

        $this->Validator->validateEmail($email);

        $admin_found = $this->db->select()
        ->where('email', $email)
        ->where('password', sha1($password))
        ->get('admins')
        ->row();

        if (empty($admin_found)) {
        	
            $r['success'] = false;
            $r['message'] = 'Email or password incorect.';

        } else {
        	$api_key = $this->genRandStr();
            
        	$data = array(
                'id' => $admin_found->id,
	        	'api_key' => $api_key,
	        	'ip_last' => $_SERVER['REMOTE_ADDR'],
	        	'date_last' => date('Y-m-d H:i:s')
        	);
            $this->db->where('id', $admin_found->id)->update('admins', $data);

        	$r['api_key'] = $api_key;
        	$r['last_login'] = $admin_found->date_last;
            $r['success'] = true;
            $r['message'] = 'OK';

        }
        $this->set($r);
	}

	/**
	 * Admin Registration
	 * url - /register
	 * method - POST
	 * params - username, email, password
	 */
	function register() {
		$this->isAuthAdmin();
		// check for required params
        $this->Validator->verifyRequiredParams(array('username', 'email', 'password'));

        $r = array();

        // reading post params
        $username = $this->app->request->post('username');
        $email = $this->app->request->post('email');
        $password = $this->app->request->post('password');

        // validating email address
        $this->Validator->validateEmail($email);

        $count_username = $this->db->where('username', $username)->count_all_results('admins');
        $count_email = $this->db->where('email', $email)->count_all_results('admins');

        if ($count_username > 0) {
            $r['success'] = false;
            $r['message'] = 'Sorry, this username already existed';
        } else if ($count_email > 0) {
            $r['success'] = true;
            $r['message'] = 'Sorry, this email already existed';
        } else {
        	$this->Admin->save(array(
        		'username' => $username,
        		'email' => $email,
        		'password' => sha1($password),
        		'ip_last' => $_SERVER['REMOTE_ADDR'],
        		'date_created' => date('Y-m-d H:i:s')
        	));
            
            $r['success'] = true;
            $r['message'] = 'OK';

        	$this->status_code = 201;
        }

        $this->set($r);
	}

}