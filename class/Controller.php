<?php

/**
* Controller
*/
class Controller
{
	public $app;
	public $status_code = 200;
	private $response = array();
	public static $json = true;
	public static $encrypt = true;

	public function __construct($app) {
		$this->data = new StdClass();
		
		$this->app = $app;

		$this->db = &DB();

		$this->Validator = new Validator($this);

		$this->set('error', false);
	}

	public function set($keys, $value = null) {
		if (is_array($keys)) {
			$this->response += $keys;
		} else {
			$this->response[$keys] = $value;
		}
	}

	public function posted($key)
    {
        if (isset($this->data->$key)) {
        	return $this->data->$key;
        }
        return null;
    }

	/**
	 * Adding Middle Layer to authenticate every request
	 * Checking if the request has valid api key in the 'Authorization' header
	*/
	public function isAuthAdmin($container = 'header', $name = 'Authorization') {
	    // Getting request headers
	    $vars = array();
	    if ($container == 'header') {
	    	$vars = apache_request_headers();
	    } else if ($container == 'get') {
	    	$vars = $_GET;
	    } else if ($container == 'post') {
	    	$vars = $_POST;
	    } else if ($container == '*') {
	    	$vars += apache_request_headers();
	    	$vars += $_GET;
	    	$vars += $_POST;
	    }

	    // Verifying Authorization Header
	    if (isset($vars[$name])) {
	        $api_key = $vars[$name];

        	$admin = $this->db->select()
	        ->where('api_key', $api_key)
	        ->get('admins')
	        ->row();

	        // validating api key
	        if (!empty($admin)) {
	            $this->admin = $admin;

	            $this->db->where('id', $admin->id)->update('admins', array(
		            'last_time' => time(),
		        ));

	        } else {
		        $this->stopError('Access Denied. Invalid Api key', 401);
	        }
	    } else {
	        // api key is missing in header
	        $this->stopError('Api key is misssing', 400);
	    }
	}

	public function isAuthMachine($container = 'header', $name = 'Authorization') {
	    $vars = array();
	    if ($container == 'header') {
	    	$vars = apache_request_headers();
	    } else if ($container == 'get') {
	    	$vars = $_GET;
	    } else if ($container == 'post') {
	    	$vars = $_POST;
	    } else if ($container == '*') {
	    	$vars += apache_request_headers();
	    	$vars += $_GET;
	    	$vars += $_POST;
	    }

	    if (isset($vars[$name])) {
	    	$api_key = $vars[$name];

	        $machine = $this->db->select()
	        ->where('api_key', $api_key)
	        ->get('pcs')
	        ->row();
	        // validating api key
	        if (!empty($machine)) {
	            $this->machine = $machine;

	            $this->db->where('id', $machine->id)->update('pcs', array(
		            'last_time' => time(),
		        ));

	        } else {
		        $this->stopError('Access Denied. Invalid Api key', 401);
	        }
	    } else {
	        // api key is missing in header
	        $this->stopError('Api key is misssing', 400);
	    }
	}

	public function genRandStr($length = 25) {
	    $characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
	    $charactersLength = strlen($characters);
	    $rString = '';
	    for ($i = 0; $i < $length; $i++) {
	        $rString .= $characters[rand(0, $charactersLength - 1)];
	    }
	    return $rString;
    }

    public function getCountry()
	{
		$gi = geoip_open(HELPER . DS . 'GeoIP.dat', GEOIP_STANDARD);
		$country = geoip_country_name_by_addr($gi, $this->getIP());
		geoip_close($gi);
		$country = empty($country) ? 'NA' : $country;
		return $country;
	}
	public function getCountryCode()
	{
		$gi = geoip_open(HELPER . DS . 'GeoIP.dat', GEOIP_STANDARD);
		$country = geoip_country_code_by_addr($gi, $this->getIP());
		geoip_close($gi);
		$country = empty($country) ? 'NA' : $country;
		return $country;
	}
	public function getIP()
	{
		if (isset($this->ip)) {
			return $this->ip;
		}
		$client  = @$_SERVER['HTTP_CLIENT_IP'];
		$forward = @$_SERVER['HTTP_X_FORWARDED_FOR'];
		$remote  = $_SERVER['REMOTE_ADDR'];
		if(filter_var($client, FILTER_VALIDATE_IP)){
		  $ip = $client;
		}else if(filter_var($forward, FILTER_VALIDATE_IP)){
		  $ip = $forward;
		}else{
		  $ip = $remote;
		}
		$this->ip = $ip;
		return $ip;
	}

	public function loadModel($name){
		$file = CLASS_ . DS . 'Models' . DS . $name . '.php';
		require_once $file;
		if(!isset($this->$name)){
			$this->$name = new $name();
		}
	}

	function stopError($msg, $status_code = null) {
		$this->app->status(!is_null($status_code) ? $status_code : $this->status_code);

		if (Conf::$debug > 0) {
			self::$encrypt = false;
		}

		$this->response['error'] = true;
        $this->response['message'] = $msg;

        $this->echoRespnse();
        $this->app->stop();
	}

	/**
	 * Renvoie json reponse au client
	 * @param String $status_code Le code http de la reponse
	 * @param Array $response array sera convertie en Json
	 */
	public function echoRespnse() {
		$this->app->status($this->status_code);

	    if (self::$json) {
	    	// setting response content type to json
		    $this->app->contentType('application/json');

		    $json = json_encode($this->response);
		    if (self::$encrypt) {
		    	$crypter = new CrypterRC4();
		    	$json = $crypter->Encrypt($json, Conf::$encryptionKey);
		    	$json = array('d' => $json);
		    	echo json_encode($json);
		    } else {
		    	echo $json;
		    }
	    }

	}

}