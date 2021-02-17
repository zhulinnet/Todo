import request from "@/util/request";

/**
 * 新增
 * @param {} params
 */
export const add = (listid, params) => {
  return request.post("/todolist/" + listid + "/todoitem", params);
};
/**
 * 完成
 * @param {} params
 */
export const complete = (listid, itemid, params) => {
  return request.post(
    "/todolist/" + listid + "/todoitem/done?itemid=" + itemid,
    params
  );
};
/**
 * 获取列表
 * @param {*} params
 */
export const getList = (listid, params) => {
  return request.get("/todolist/" + listid + "/todoitem", { params: params });
};
