wx.ready(function () {
    //调用扫码
    wx.scanQRCode();
})
wx.error(function (res) {
    alert(res.errMsg);
});
