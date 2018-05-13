<?php
class CrypterRC4{
   public function Encrypt($data, $key) {
      return base64_encode($this->rc4($data, $key));
   }
   public function Decrypt($data, $key){
      return $this->rc4(base64_decode($data), $key);
   }

   private function rc4($data, $key) {
      $s = array();
      for ($i = 0; $i < 256; $i++) {
         $s[$i] = $i;
      }
      $j = 0;
      for ($i = 0; $i < 256; $i++) {
         $j = ($j + $s[$i] + ord($key[$i % strlen($key)])) % 256;
         $x = $s[$i];
         $s[$i] = $s[$j];
         $s[$j] = $x;
      }
      $i = 0;
      $j = 0;
      $res = '';
      for ($y = 0; $y < strlen($data); $y++) {
         $i = ($i + 1) % 256;
         $j = ($j + $s[$i]) % 256;
         $x = $s[$i];
         $s[$i] = $s[$j];
         $s[$j] = $x;
         $res .= $data[$y] ^ chr($s[($s[$i] + $s[$j]) % 256]);
      }
      return $res;
   }

}