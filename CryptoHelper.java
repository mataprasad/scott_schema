package com.lions.attendance.core.util;

import java.security.Key;
import javax.crypto.Cipher;
import javax.crypto.spec.SecretKeySpec;

import org.apache.commons.codec.binary.Base64;
import sun.misc.BASE64Decoder;
import sun.misc.BASE64Encoder;

public class CryptoHelper {

    private static final String ALGORITHM = "AES";
    /*
    <script>
    function RANDOM_KEY_VALUE() {
        var input = "MATAPRASAD104599";
        var n = input.length;
        var output = "";
        for (var i = 0; i < n; i++) {
            var pos = Math.floor((Math.random() * n) + 1) - 1;
            output += input[pos];
        }
        console.log(output);
        alert(output);
    }
    </script>
     */
    private static final byte[] KEY_VALUE = "AM4DA9SM1TAAADTS".getBytes();

    public static void main(String args[]) throws Exception {
        String input = "Text to encryp";
        System.out.println("input:" + input);
        String encriptedValue = encrypt(input);
        //encriptedValue = "f3315a7a3db3b9e59c6d6ca8720282cf668b152a109868395d5248a3d172ca45daf207ed0e3569ec0ffb7b373d4876ef4879524ef651850732b8c7dcef34774047c48a767efd155a8cbf76d207e76ee2495efc8d9d89eb631656eb1cf3cb75a9e1fec7ca684ffbe96199eeca1601ca83351acff834f50f6bf6c658350e1f1cefb109990b108af4797dd6ef6bc2534572d412525bb0f73584a7fa66c986981cb34231c7f176c7cb9ca2f9ecf1017a2677";
        
        System.out.println("cipher:" + encriptedValue);
        encriptedValue = "n0c0KYNl+PM6cekV7YMHI5b0fLodT3jU27cHt7+VGrA=";
        System.out.println("cipher:" + encriptedValue);
        String decriptedValue = decrypt(encriptedValue);
        System.out.println("output:" + decriptedValue.replace(" ", ""));
        


    }

    public static String encrypt(String valueToEncrypt) throws Exception {
        MCrypt mCrypt = new MCrypt();
        return Base64.encodeBase64String(mCrypt.encrypt(valueToEncrypt));
        //return MCrypt.bytesToHex(mCrypt.encrypt(valueToEncrypt));
    }

    public static String decrypt(String valueToDecrypt) throws Exception {
        MCrypt mCrypt = new MCrypt();
        return new String(mCrypt.decrypt(valueToDecrypt)).replace(" ", "");
        
        //return new String(mCrypt.decrypt(encoder. valueToDecrypt)).replace(" ", "");
    }

    private static String encrypt_1(String valueToEncrypt) throws Exception {
        Key key = generateKey();
        Cipher c = Cipher.getInstance(ALGORITHM);
        c.init(Cipher.ENCRYPT_MODE, key);

        byte[] encryptedValue = c.doFinal(valueToEncrypt.getBytes());
        byte[] encodedValue = new Base64().encode(encryptedValue);

        return new String(encodedValue);
    }

    private static String decrypt_1(String valueToDecrypt) throws Exception {
        Key key = generateKey();
        Cipher c = Cipher.getInstance(ALGORITHM);
        c.init(Cipher.DECRYPT_MODE, key);

        byte[] decodedValue = new Base64().decode(valueToDecrypt.getBytes());
        byte[] decryptedVal = c.doFinal(decodedValue);

        return new String(decryptedVal);
    }

    private static Key generateKey() throws Exception {
        Key key = new SecretKeySpec(KEY_VALUE, ALGORITHM);
        return key;
    }
}
