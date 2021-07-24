//数组转化为Json
function array2dToJson(a, p, nl) {
    var i, j, s = '{"' + p + '":[';
    nl = nl || '';
    for (i = 0; i < a.length; ++i) {
        s += nl + array1dToJson(a[i]);
        if (i < a.length - 1) {
            s += ',';
        }
    }
    s += nl + ']}';
    return s;
}

//数组转化为Json
function array1dToJson(a, p) {
    var i, s = '[';
    for (i = 0; i < a.length; ++i) {
        if (a[i]) {
            if (typeof a[i] == 'string') {
                s += '"' + a[i] + '"';
            } else { // assume number type
                s += a[i];
            }
            if (i < a.length - 1) {
                s += ',';
            }
        }
    }
    s += ']';
    if (p) {
        return '{"' + p + '":' + s + '}';
    }
    return s;
}