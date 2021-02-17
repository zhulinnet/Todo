import request from "@/util/request";

// 登录
export const login = (params) => {
  return request.post("account/login", params);
};
// 注册
export const register = (params) => {
  return request.post("account", params);
};
