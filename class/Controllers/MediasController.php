<?php
class MediasController extends Controller {

    function gets($pc_id = null) {
        $this->isAuthAdmin();

        $this->db->select('pcs.name as pc_name,medias.*')->order_by('date_created', 'DESC');
        if (is_numeric($pc_id)) {
            $this->db->where('pc_id', $pc_id);
        }
        $this->db->where('adm', 0)->join('pcs', 'pcs.id = medias.pc_id');
        $medias = $this->db->get('medias')
        ->result();

        $r['medias'] = $medias;
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->set($r);
    }

    function get($media_id = null) {
        $this->isAuthAdmin('*', 'token');

        $media = $this->db->select()
        ->order_by('date_created', 'DESC')
        ->where('id', $media_id)
        ->get('medias')
        ->row();

        self::$json = false;

        if (!empty($media)) {
            $dir = ($media->adm == 1) ? 'uploads': 'pcs';
            $path = DATA.DS.$dir.DS.$media->file;
            $this->app->contentType(mime_content_type($path));

            readfile($path);
        } else {
            $this->app->contentType(mime_content_type(DATA.DS.'default.png'));
            readfile(DATA.DS.'default.png');
        }
    }

    function delete($media_id = null) {
        $this->isAuthAdmin();
        $r = array();

        $media = $this->db->select()
        ->where('id', $media_id)
        ->get('medias')
        ->row();

        if (!empty($media)) {
            $dir = ($media->adm == 1) ? 'uploads': 'pcs';
            $path = DATA.DS.$dir.DS.$media->file;

            if (file_exists($path)) {
                unlink($path);
            }
            $this->db->delete('medias', array('id' => $media_id));
            $r['success'] = false;
            $r['message'] = 'OK';
        } else {
            $r['success'] = true;
            $r['message'] = 'N\'existe pas';
        }
        $this->set($r);
    }

    //FOR CLIENT ONLY
    function get_file($media_id = null) {
        $this->isAuthMachine('*', 'token');

        $media = $this->db->select()
        ->order_by('date_created', 'DESC')
        ->where('pc_id', $this->machine->id)
        ->where('id', $media_id)
        ->get('medias')
        ->row();

        self::$json = false;
        if (!empty($media)) {
            $dir = ($media->adm == 1) ? 'uploads': 'pcs';
            $path = DATA.DS.$dir.DS.$media->file;
            $this->app->contentType(mime_content_type($path));

            readfile($path);
        } else {
            $this->app->contentType(mime_content_type(DATA.DS.'default.png'));
            readfile(DATA.DS.'default.png');
        }
    }
	
    function post() {
        $this->isAuthMachine();
        $r = array();

        $headers = apache_request_headers();
        $context = '';

        if (!isset($headers['context'])) {
            $this->Validator->verifyRequiredParams(array('context'), false);
        }else{
            $context = $headers['context'];
        } if (empty($_FILES['file']['name'])) {
            $this->Validator->verifyRequiredParams(array('file_name'), false);
        }

        $file_name = basename($_FILES['file']['name']);
        $extension = pathinfo(basename($file_name), PATHINFO_EXTENSION);
        $r_name = '';
        $allow_multiple = true;

        switch ($context) {
            case 'screen_min':
                $r_name = str_replace('-', '_', $this->machine->guid);
                $extension = 'png';
                $allow_multiple = false;
                break;

            case 'screen':
                $r_name = $this->genRandStr(5) . '_' . date('Y-m-d-H_i');
                $extension = 'png';
                break;

            case 'file':
                $r_name = $file_name;
                break;
        }

        //met dans le dossier sauf pour screen_min
        $dir = $this->machine->name;
        if (!in_array($context, array('screen_min'))) {
            $dir .= DS.$context;
        }

        //verfication des dossier si il existe pas on les crée; pour chaque PC un dossier
        if(!file_exists(dirname(DATA.DS.'pcs'.DS.$dir))) {
            mkdir(dirname(DATA.DS.'pcs'.DS.$dir));
        } if(!file_exists(DATA.DS.'pcs'.DS.$dir)) {
            mkdir(DATA.DS.'pcs'.DS.$dir, 0777);
        }

        //Relative chemin pour la db et final_path le chemin entier
        $rel_path = $dir.DS.basename($r_name, ".".$extension).'.'.$extension;
        $final_path = DATA.DS.'pcs'.DS.$rel_path;

        //Verfier si le nom du fichier existe on le duplique en ajoutant une index logique
        if(file_exists($final_path)) {
            if ($allow_multiple) {
                $ok = false;
                $i=1;
                while (!$ok) {
                    if(file_exists($final_path)) {
                        $rel_path = $dir . DS . basename($r_name, ".".$extension) . '_'.$i.'.' . $extension;
                        $final_path = DATA.DS.'pcs'.DS.$rel_path;
                    }else{
                        $ok = true;
                    }
                    $i++;
                }
            } else {
               unlink($final_path); 
            }
        }
        
        move_uploaded_file($_FILES['file']['tmp_name'], $final_path);

        //Correction extension fichier
        $r_name = basename($r_name, ".".$extension) . '_'.$i.'.' . $extension;

        switch ($context) {
            case 'screen_min':
                $this->resize(120, $final_path, $final_path);
                $this->compress($final_path, $final_path, 80);
                $this->db->where('id', $this->machine->id)->update('pcs', array(
                    'picture' => $rel_path,
                ));
                break;

            case 'screen':
            case 'file':
                if ($context == 'screen' || $extension == 'png' || $extension == 'jpg' || $extension == 'jprg') {
                    $this->compress($final_path, $final_path, 80);
                }
                $rel_path = str_replace(DS, '/', $rel_path);
                $this->db->insert('medias', array(
                    'pc_id' => $this->machine->id,
                    'name' => $r_name,
                    'file' => $rel_path,
                    'extension' => $extension,
                    'context' => $context,
                    'adm' => 0,
                    'date_created' => date('Y-m-d H:i:s'),
                ));
                $r['media_id'] = $this->db->insert_id();
                break;
        }
        
        $r['path'] = $rel_path;
        $r['success'] = true;
        $r['message'] = 'OK';

        $this->status_code = 201;
        $this->set($r);
    }

    function post_admin() {
        $this->isAuthAdmin();
        $r = array();

        $headers = apache_request_headers();
        $context = '';
        $pc_id = '';

        if (!isset($headers['context'])) {
            $this->Validator->verifyRequiredParams(array('context'), false);
        } else {
            $context = $headers['context'];
        }
        if (!isset($headers['pc_id'])) {
            $this->Validator->verifyRequiredParams(array('pc_id'), false);
        } else {
            $pc_id = $headers['pc_id'];
        } 
        if (empty($_FILES['file']['name'])) {
            $this->Validator->verifyRequiredParams(array('file_name'), false);
        }

        $file_name = basename($_FILES['file']['name']);
        $extension = pathinfo(basename($file_name), PATHINFO_EXTENSION);

        //met dans le dossier sauf pour screen_min
        $dir = date('Y-m-d');

        //verfication des dossier si il existe pas on les crée; pour chaque jours
        if(!file_exists(DATA.DS.'uploads'.DS.$dir)) {
            mkdir(DATA.DS.'uploads'.DS.$dir, 0777);
        }

        //Relative chemin pour la db et final_path le chemin entier
        $rel_path = $dir.DS.$file_name.'.'.$extension;
        $final_path = DATA.DS.'uploads'.DS.$rel_path;

        //Verfier si le nom du fichier existe on le duplique en ajoutant une index logique
        $ok = false;
        $i=1;
        while (!$ok) {
            if(file_exists($final_path)) {
                $rel_path = $dir . DS . basename($file_name, ".".$extension) . '_'.$i.'.' . $extension;
                $final_path = DATA.DS.'uploads'.DS.$rel_path;
            }else{
                $ok = true;
            }
            $i++;
        }

        move_uploaded_file($_FILES['file']['tmp_name'], $final_path);
        
        if ($context == 'screen' || $extension == 'png' || $extension == 'jpg' || $extension == 'jprg') {
            $this->compress($final_path, $final_path, 80);
        }
        $rel_path = str_replace(DS, '/', $rel_path);
        $this->db->insert('medias', array(
            'pc_id' => $pc_id,
            'name' => $file_name,
            'file' => $rel_path,
            'extension' => $extension,
            'context' => $context,
            'adm' => 1,
            'date_created' => date('Y-m-d H:i:s'),
        ));
        $r['media_id'] = $this->db->insert_id();
              
        $r['path'] = $rel_path;
        $r['success'] = true;
        $r['message'] = 'OK';
        
        $this->status_code = 201;
        $this->set($r);
    }

    protected function compress($source, $destination, $quality = 50) {
        $info = getimagesize($source);
        $image = null;

        switch ($info['mime']) {
            case 'image/jpeg':
                $image = imagecreatefromjpeg($source);
                break;

            case 'image/gif':
                $image = imagecreatefromgif($source);
                break;

            case 'image/gif':
                $image = imagecreatefrompng($source);
                break;

            case 'image/png':
                $image = imagecreatefrompng($source);
                break;
        }

        imagejpeg($image, $destination, $quality);

        return $destination;
    }
    protected function resize($newWidth, $targetFile, $originalFile) {
        $info = GetImageSize($originalFile);
        $mime = $info['mime'];

        switch ($mime) {
                case 'image/jpeg':
                        $image_create_func = 'imagecreatefromjpeg';
                        $image_save_func = 'imagejpeg';
                        $new_image_ext = 'jpg';
                        break;

                case 'image/png':
                        $image_create_func = 'imagecreatefrompng';
                        $image_save_func = 'imagepng';
                        $new_image_ext = 'png';
                        break;

                case 'image/gif':
                        $image_create_func = 'imagecreatefromgif';
                        $image_save_func = 'imagegif';
                        $new_image_ext = 'gif';
                        break;

                default: 
                        throw new Exception('Unknown image type.');
        }

        $img = $image_create_func($originalFile);
        list($width, $height) = getimagesize($originalFile);

        $newHeight = ($height / $width) * $newWidth;
        $tmp = imagecreatetruecolor($newWidth, $newHeight);
        imagecopyresampled($tmp, $img, 0, 0, 0, 0, $newWidth, $newHeight, $width, $height);

        if (file_exists($targetFile)) {
            unlink($targetFile);
        }
        $image_save_func($tmp, $targetFile);
    }

}