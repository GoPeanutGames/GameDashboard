mergeInto(LibraryManager.library, {
    IsMobile: function(){
        return /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
    },

    Login: async function (isDev, cbSuccess, cbFail) {
        let chainId = await window.ethereum.request({ method: "eth_chainId" });
        let selectedChainId = isDev ? "0x13881" : "0x89";
        if (chainId !== selectedChainId) {
            await window.ethereum.request({
                method: "wallet_addEthereumChain",
                params: [
                    {
                        chainId: isDev ? "0x" + (80001).toString(16) : "0x" + (137).toString(16),
                        rpcUrls: isDev ? ["https://rpc-mumbai.maticvigil.com"] : ["https://polygon-rpc.com"],
                        chainName: isDev ? "Matic Mumbai" : "Polygon Mainnet",
                        nativeCurrency:{
                            name: "MATIC",
                            symbol: "MATIC",
                            decimals: 18
                        },
                        blockExplorerUrls: isDev ? ["https://mumbai.polygonscan.com/"] : ["https://polygonscan.com/"]
                    },
                ],
            });
            let chainId = await window.ethereum.request({ method: "eth_chainId" });
            if (chainId !== selectedChainId) {
                let e  = "Chain switch cancelled";
                const bufferSize = lengthBytesUTF8(e) + 1;
                const buffer = _malloc(bufferSize);
                stringToUTF8(e, buffer, bufferSize);
                Module.dynCall_vi(cbFail, [buffer]);
                return;
            }
        }
        try {
            let userAddress;
            const accounts = await ethereum.request({ method: "eth_requestAccounts" });
            
            if(accounts.length === 0) {
                let e  = "No wallet has been connected";
                const bufferSize = lengthBytesUTF8(e) + 1;
                const buffer = _malloc(bufferSize);
                stringToUTF8(e, buffer, bufferSize);
                Module.dynCall_vi(cbFail, [buffer]);
                return;
            }
        
            userAddress = accounts[0];
            
            const bufferSize = lengthBytesUTF8(userAddress) + 1;
            const buffer = _malloc(bufferSize);
            stringToUTF8(userAddress, buffer, bufferSize);
            Module.dynCall_vi(cbSuccess, [buffer]);
        
        } catch (e) {
            const bufferSize = lengthBytesUTF8(e) + 1;
            const buffer = _malloc(bufferSize);
            stringToUTF8(e, buffer, bufferSize);
            Module.dynCall_vi(cbFail, [buffer]);
        }
    },
    
    RequestSignature: async function (schema, account, cbSuccess, cbFail) {
        account = UTF8ToString(account);
        schema = UTF8ToString(schema);
        
        try{
            const signature = await ethereum.request({
                method: "eth_signTypedData_v4",
                params: [account, schema],
                from: account,
            });
            const bufferSize = lengthBytesUTF8(signature) + 1;
            const buffer = _malloc(bufferSize);
            stringToUTF8(signature, buffer, bufferSize);
            Module.dynCall_vi(cbSuccess, [buffer]);
        }
        catch (e) {
            const bufferSize = lengthBytesUTF8(e) + 1;
            const buffer = _malloc(bufferSize);
            stringToUTF8(e, buffer, bufferSize);
            Module.dynCall_vi(cbFail, [buffer]);
        }
    }
});