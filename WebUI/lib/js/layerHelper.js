function OpenPape(url, title) {
    var index = layer.open({
        type: 2,
        title: title,
        maxmin: true, //开启最大化最小化按钮
        content: url,
        area: ['90%', '90%']
    });
}

function OpenPape(url, title, width,hight) {
    var index = layer.open({
        type: 2,
        title: title,
        maxmin: true, //开启最大化最小化按钮
        content: url,
        area: [width, hight]
    });
}
