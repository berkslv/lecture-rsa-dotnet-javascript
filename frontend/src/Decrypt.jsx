import { useState } from "react";
import { decryptRsa } from "./utils/RsaEncryptionHelper";
import { privateKeyPem } from "./utils/constants";

function Decrypt() {
  const [privateKey, setPrivateKey] = useState(privateKeyPem);
  const [data, setData] = useState("");
  const [encryptedData, setEncryptedData] = useState("");

  const handleEncrypt = async () => {
    try {
      const encryptedData = await decryptRsa(data, privateKey);
      setEncryptedData(encryptedData);
    } catch (e) {
      setEncryptedData("Encryption error");
    }
  };

  return (
    <>
      <div className="mt-10 grid grid-cols-12 gap-2 w-full">
        <div className="col-span-12 md:col-span-6">
          <label className="block mb-2 text-sm font-medium text-slate-900 dark:text-white">
            Private Key
          </label>
          <textarea
            value={privateKey}
            onChange={(e) => setPrivateKey(e.target.value)}
            rows="12"
            className="block p-2.5 w-full text-sm text-slate-900 bg-slate-50 rounded-lg border border-slate-300 focus:ring-slate-500 focus:border-slate-500 dark:bg-slate-700 dark:border-slate-600 dark:placeholder-slate-400 dark:text-white dark:focus:ring-slate-500 dark:focus:border-slate-500"
            placeholder="Write product description here"
          ></textarea>
        </div>
        <div className="col-span-12 md:col-span-6">
          <label className="block mb-2 text-sm font-medium text-slate-900 dark:text-white">
            Data to Decrypt
          </label>
          <textarea
            value={data}
            onChange={(e) => setData(e.target.value)}
            rows="12"
            className="block p-2.5 w-full text-sm text-slate-900 bg-slate-50 rounded-lg border border-slate-300 focus:ring-slate-500 focus:border-slate-500 dark:bg-slate-700 dark:border-slate-600 dark:placeholder-slate-400 dark:text-white dark:focus:ring-slate-500 dark:focus:border-slate-500"
            placeholder="Write product description here"
          ></textarea>
        </div>
      </div>
      <div className="mt-5 flex justify-center">
        <button
          onClick={handleEncrypt}
          type="submit"
          className="text-white inline-flex items-center bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
        >
          Decrypt
        </button>
      </div>
      <div className="grid grid-cols-12 gap-2 w-full">
        <div className="col-span-12">
          <label className="block mb-2 text-sm font-medium text-slate-900 dark:text-white">
            Encrypted Data
          </label>
          <textarea
            value={encryptedData}
            rows="4"
            className="block p-2.5 w-full text-sm text-slate-900 bg-slate-50 rounded-lg border border-slate-300 focus:ring-slate-500 focus:border-slate-500 dark:bg-slate-700 dark:border-slate-600 dark:placeholder-slate-400 dark:text-white dark:focus:ring-slate-500 dark:focus:border-slate-500"
            placeholder="Write product description here"
          ></textarea>
        </div>
      </div>
    </>
  );
}

export default Decrypt;
