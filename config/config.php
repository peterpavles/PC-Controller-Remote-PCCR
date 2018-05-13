<?php
date_default_timezone_set('Europe/Paris');

class Conf
{
	static $debug = 3;
	static $encryptionKey = 'piWUCi7DsWLUTwOLou9QbiBP33NVk3yAyWRrAIXrdtg';

    static $database_active_group = 'default';
	static $database = array(
		'default' => array(
			'dsn'   => '',
		    'hostname' => 'localhost',
		    'username' => 'root',
		    'password' => '',
		    'database' => 'pcc_db',
		    'dbdriver' => 'mysqli',
		    'dbprefix' => '',
		    'pconnect' => FALSE,
		    'db_debug' => TRUE,
		    'cache_on' => FALSE,
		    'cachedir' => '',
		    'char_set' => 'utf8',
		    'dbcollat' => 'utf8_general_ci',
		    'swap_pre' => '',
		    'encrypt' => FALSE,
		    'compress' => FALSE,
		    'stricton' => FALSE,
		    'failover' => array(),
		    'save_queries' => TRUE
		)
	);
}
