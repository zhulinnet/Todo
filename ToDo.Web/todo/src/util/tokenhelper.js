const tokenkey = "token";
let TokenHelper = {

    /**
     * 获取 token
     */
    getToken(){
        return localStorage.getItem(tokenkey);
    },

    /**
     * 设置token
     * @param {*} tokeValue 
     */
    setToken(tokeValue){
        localStorage.setItem(tokenkey, tokeValue);
    },
    deleteToken(){
        localStorage.removeItem(tokenkey);
    }
}

export default TokenHelper;