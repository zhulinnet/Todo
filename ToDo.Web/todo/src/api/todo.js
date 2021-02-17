import request from "@/util/request";

/**
 * 新增
 * @param {} params
 */
export const add = (params) => {
  return request.post("/todolist", params);
};
/**
 * 新增分享
 * @param {} params
 */
export const addShare = (params) => {
  return request.post("/todoshare", params);
};
/**
 * 获取列表
 * @param {*} params
 */
export const getList = (params) => {
  return request.get("/todolist", { params: params });
};
/**
 * 获取分享列表
 * @param {*} params
 */
export const getSahreList = (params) => {
  return request.get("/todoshare", { params: params });
};
