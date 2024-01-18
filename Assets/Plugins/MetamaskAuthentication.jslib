mergeInto(LibraryManager.library, {
    IsMobile: function(){
        return /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
    },

    Login: async function (isDev, obj) {
        let chainId = await window.ethereum.request({ method: "eth_chainId" });
        if (chainId !== 80001) {
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
        }
        try {
            let userAddress;
            const accounts = await ethereum.request({ method: "eth_requestAccounts" });
            
            if(accounts.length === 0) {
                console.log("No wallet has been connected");
                return;
            }
        
            userAddress = accounts[0];
            
            const bufferSize = lengthBytesUTF8(userAddress) + 1;
            const buffer = _malloc(bufferSize);
            stringToUTF8(userAddress, buffer, bufferSize);
            Module.dynCall_vi(obj, [buffer]);
        
        } catch (e) {
            console.log("Error Connecting: " + e);
        }
    },
    
    RequestSignature: async function (schema, account, obj) {
        account = UTF8ToString(account);
        schema = UTF8ToString(schema);
        
        const signature = await ethereum.request({
            method: "eth_signTypedData_v4",
            params: [account, schema],
            from: account,
        });
        
        const bufferSize = lengthBytesUTF8(signature) + 1;
        const buffer = _malloc(bufferSize);
        stringToUTF8(signature, buffer, bufferSize);
        Module.dynCall_vi(obj, [buffer]);
    },
    
    GetURLFromPage: function () {
        var returnStr = window.top.location.href;
        var bufferSize = lengthBytesUTF8(returnStr) + 1
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer;
    },
});