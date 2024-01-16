mergeInto(LibraryManager.library, {

    Login: async function (obj) {
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
    }
});