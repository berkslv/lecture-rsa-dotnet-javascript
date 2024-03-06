const encryptAlgorithm = {
  name: "RSA-OAEP",
  modulusLength: 2048,
  publicExponent: new Uint8Array([1, 0, 1]),
  extractable: true,
  hash: {
    name: "SHA-256",
  },
};

function arrayBufferToBase64(arrayBuffer) {
  const byteArray = new Uint8Array(arrayBuffer);
  let byteString = "";
  for (let i = 0; i < byteArray.byteLength; i++) {
    byteString += String.fromCharCode(byteArray[i]);
  }
  const b64 = window.btoa(byteString);

  return b64;
}

function base64StringToArrayBuffer(b64str) {
  const byteStr = atob(b64str);
  const bytes = new Uint8Array(byteStr.length);
  for (let i = 0; i < byteStr.length; i++) {
    bytes[i] = byteStr.charCodeAt(i);
  }
  return bytes.buffer;
}

function convertPemToArrayBuffer(pem) {
  const lines = pem.split("\n");
  let encoded = "";
  for (let i = 0; i < lines.length; i++) {
    if (
      lines[i].trim().length > 0 &&
      lines[i].indexOf("-----BEGIN RSA PRIVATE KEY-----") < 0 &&
      lines[i].indexOf("-----BEGIN PRIVATE KEY-----") < 0 &&
      lines[i].indexOf("-----BEGIN PUBLIC KEY-----") < 0 &&
      lines[i].indexOf("-----END RSA PRIVATE KEY-----") < 0 &&
      lines[i].indexOf("-----END PRIVATE KEY-----") < 0 &&
      lines[i].indexOf("-----END PUBLIC KEY-----") < 0
    ) {
      encoded += lines[i].trim();
    }
  }
  console.log(encoded);
  return base64StringToArrayBuffer(encoded);
}

export const encryptRsa = async (str, pemString) => {
  try {
    // convert string into ArrayBuffer
    const encodedPlaintext = new TextEncoder().encode(str).buffer;
    const keyArrayBuffer = convertPemToArrayBuffer(pemString);
    // import public key
    const secretKey = await crypto.subtle.importKey(
      "spki",
      keyArrayBuffer,
      encryptAlgorithm,
      true,
      ["encrypt"]
    );
    // encrypt the text with the public key
    const encrypted = await crypto.subtle.encrypt(
      {
        name: "RSA-OAEP",
      },
      secretKey,
      encodedPlaintext
    );

    // store data into base64 string
    return arrayBufferToBase64(encrypted);
  } catch (error) {
    console.error("Encryption Error:", error);
  }
};

export const decryptRsa = async (str, pemString) => {
  try {
    // convert base64 encoded input string into ArrayBuffer
    const encodedPlaintext = base64StringToArrayBuffer(str);
    const keyArrayBuffer = convertPemToArrayBuffer(pemString);
    // import private key
    const secretKey = await crypto.subtle.importKey(
      "pkcs8",
      keyArrayBuffer,
      encryptAlgorithm,
      true,
      ["decrypt"]
    );

    // decrypt the text with the public key
    const decrypted = await crypto.subtle.decrypt(
      {
        name: "RSA-OAEP",
      },
      secretKey,
      encodedPlaintext
    );

    // decode the decrypted ArrayBuffer output
    const uint8Array = new Uint8Array(decrypted);
    const textDecoder = new TextDecoder();
    const decodedString = textDecoder.decode(uint8Array);
    return decodedString;
  } catch (error) {
    console.error("Decryption Error:", error);
  }
};
