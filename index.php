<?php

	define('ROOT', dirname(__FILE__));
	define('DS', DIRECTORY_SEPARATOR);
	define('CLASS_', ROOT.DS.'class');
	define('HELPER', ROOT.DS.'helpers');
	define('DATA', ROOT.DS.'datas');

	require CLASS_.DS.'Includer.php';
	require CLASS_.DS.'vendor'.DS.'autoload.php';
	
	$app = new \Slim\Slim();

	// Only invoked if mode is "development"
	$app->configureMode('development', function () use ($app) {
	    $app->config(array(
	        'log.enable' => false,
	        'debug' => true
	    ));
	});

	$router = new Router($app);
	$router->get('/', 'Admin@index')->name('root');


	/*/******************************  Admin Users *********************************/
	$router->post('/admin/register', 'Admin@register')->name('register');
	$router->post('/admin/login', 'Admin@login')->name('login');

	//Admin
	$router->get('/admin/machines', 'Machines@gets')->name('machines');
	$router->get('/admin/machines/:id', 'Machines@get')->name('machine');
	$router->get('/admin/machines/delete/:id', 'Machines@delete')->name('delete');

	/******************************  Commands *********************************/
	//Client
	$router->get('/commands', 'Commands@gets')->name('commands');

	//Admin
	$router->get('/admin/commands/:id', 'Commands@get')->name('command');
	$router->post('/admin/commands', 'Commands@post')->name('command-set');
	$router->put('/admin/commands/:id', 'Commands@put')->name('command-edit');
	$router->delete('/admin/commands/:id', 'Commands@delete')->name('command-delete');


	/******************************  Results *********************************/

	/******************************  Machines *********************************/
	//Client
	$router->post('/machines', 'Machines@post')->name('machines-register');

	//Client
	$router->post('/results', 'Results@post')->name('result-set');
	$router->put('/results/:id', 'Results@put')->name('result-edit');

	//Admin
	$router->get('/admin/results', 'Results@gets')->name('result-gets');
	$router->get('/admin/results/:id', 'Results@get')->name('result-get');
	$router->delete('/admin/results/:id', 'Results@delete')->name('result-delete');

	/******************************  Medias *********************************/
	$router->post('/admin/medias', 'Medias@post_admin')->name('medias-set');
	$router->get('/admin/medias/get/:id', 'Medias@get')->name('medias-get');
	$router->get('/admin/medias/gets', 'Medias@gets')->name('medias-gets');
	$router->get('/admin/medias/gets/:id', 'Medias@gets')->name('medias-gets');
	$router->get('/admin/medias/delete/:id', 'Medias@delete')->name('delete');

	$router->get('/medias/get/:id', 'Medias@get_file')->name('medias-get_file');
	$router->post('/medias', 'Medias@post')->name('medias-set');

	/******************************  Tools *********************************/
	$router->get('/admin/tools/ip', 'Tools@ip')->name('get-myIp');

	$app->run();

?>










