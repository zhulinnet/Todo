import axios from "axios";
import tokenhelper from "@/util/tokenhelper";
import { Message } from "element-ui";

export const requestAxios = axios.create({
  timeout: 10000,
  baseURL: "http://localhost:52431/api/",
  headers: {
    "Content-type": "application/json;charset=utf-8",
  },
});

// 拦截请求
requestAxios.interceptors.request.use(
  (config) => {
    if (config.method === "POST" || config.method === "DELETE") {
      config.data = JSON.stringify(config.data);
      console.log(config.data);
    }
    const token = tokenhelper.getToken();
    if (token) {
      config.headers.Authorization = "Bearer " + token;
    }
    return config;
  },
  (error) => {
    console.log(error);
    return Promise.reject(error);
  }
);

// 拦截响应
requestAxios.interceptors.response.use(
  (response) => {
    //接收到响应数据并成功后的一些共有的处理，关闭loading等
    return response;
  },
  (error) => {
    /***** 接收到异常响应的处理开始 *****/
    if (error && error.response) {
      if (error.response.status == 401) {
        location.href = "#/login/";
      }
      if (error.response.data == "") {
        // 1.公共错误处理
        // 2.根据响应码具体处理
        switch (error.response.status) {
          case 400:
            error.message = "错误请求";
            break;
          case 401:
            error.message = "未授权，请重新登录";
            break;
          case 403:
            error.message = "拒绝访问";
            break;
          case 404:
            error.message = "请求错误,未找到该资源";
            break;
          case 405:
            error.message = "请求方法未允许";
            break;
          case 408:
            error.message = "请求超时";
            break;
          case 500:
            error.message = "服务器端出错";
            break;
          case 501:
            error.message = "网络未实现";
            break;
          case 502:
            error.message = "网络错误";
            break;
          case 503:
            error.message = "服务不可用";
            break;
          case 504:
            error.message = "网络超时";
            break;
          case 505:
            error.message = "http版本不支持该请求";
            break;
          default:
            error.message = `连接错误${error.response.status}`;
        }
      } else {
        error.message = error.response.data;
      }
    } else {
      // 超时处理
      if (JSON.stringify(error).includes("timeout")) {
        Message.error("服务器响应超时，请刷新当前页");
      }
      error.message("连接服务器失败");
    }

    Message.error(error.message);
    /***** 处理结束 *****/
    return Promise.resolve(error.response);
  }
);

export default requestAxios;
