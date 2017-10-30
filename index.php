<?php
include_once "MCrypt.php";

$mcrypt = new MCrypt();

/* Encrypt */

$encrypted = $mcrypt->encrypt("Text to encrypt Mata Prasad");



echo ($encrypted);
echo "<br><br>";
/* Decrypt */

$decrypted = $mcrypt->decrypt("AGpL3D8gMWMEQ4tF6UzqcA==");

echo ($decrypted);

?>