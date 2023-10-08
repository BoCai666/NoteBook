## 请求（Request）
- **动作**
  - get：向服务器拉取数据
  - post：向服务器传输数据
- **参数：** 参数用&符隔开
  - url：get请求的参数放在url中，如 `http://www.baidu.com?name=jack&age=18` 格式为：地址?参数名=参数值&参数名=参数值
  - request body：post请求的参数放在请求体中

## 响应（Response）
- **status：** 状态码
  - 200：请求成功
  - 301：永久重定向
  - 302：临时重定向
  - 404：请求资源不存在
  - 500：服务器内部错误
  - 503：服务器暂时不可用
- **headers：** 响应头
- **body：** 响应体