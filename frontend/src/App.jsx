import { useState } from "react";
import Encrypt from "./Encrypt";
import Decrypt from "./Decrypt";

function App() {
  const [currentTab, setCurrentTab] = useState("encrypt");

  return (
    <div className="min-h-screen bg-slate-900 flex justify-center items-center flex-col">
      <div className="container p-10 md:p-20 w-full">
        <div>
          <h1 className="text-3xl font-bold text-white text-center">
            RSA Encryption
          </h1>
          <p className="text-slate-400 text-center mt-2">
            Encrypt data with a public key
          </p>
        </div>
        <div className="mt-10 flex justify-center space-x-2">
          <button
            onClick={() => setCurrentTab("encrypt")}
            className={`${
              currentTab == "encrypt"
                ? "bg-blue-800 text-white hover:bg-blue-700"
                : "bg-slate-700 text-slate-900 hover:bg-slate-600"
            } text-sm font-medium rounded-lg px-5 py-2.5 text-center bg-slate-600 text-white`}
          >
            Encrypt
          </button>
          <button
            onClick={() => setCurrentTab("decrypt")}
            className={`${
              currentTab == "decrypt"
                ? "bg-blue-800 text-white hover:bg-blue-700"
                : "bg-slate-700 text-slate-900 hover:bg-slate-600"
            } text-sm font-medium rounded-lg px-5 py-2.5 text-center bg-slate-600 text-white`}
          >
            Decrypt
          </button>
        </div>
        {currentTab === "encrypt" && <Encrypt />}
        {currentTab === "decrypt" && <Decrypt />}
      </div>
    </div>
  );
}

export default App;
