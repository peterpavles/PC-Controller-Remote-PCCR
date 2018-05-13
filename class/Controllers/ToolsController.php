<?php
class ToolsController extends Controller {
	
    function ip() {
        $this->isAuthAdmin();
        $r = array('your_ip' => $this->getIP(), 'success' => true, 'message' => 'OK');
        $this->set($r);
    }

}