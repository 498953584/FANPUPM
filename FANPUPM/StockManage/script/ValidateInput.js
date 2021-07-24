
//普通控件验证对象
function ValidateInput(id, name, regular) {
    this.control = document.getElementById(id);
    this.name = name;
    this.regular = regular;
}
ValidateInput.prototype.validateMustInput = function () {
    if (!this.control) {
        alert('系统提示：\n\n找不到控件:' + this.name);
        return false;
    }
    if (!this.control.value) {
        alert('系统提示：\n\n' + this.name + '必须输入！');
        return false;
    }
    else {
        return true;
    }
}
ValidateInput.prototype.validateIntFormat = function () {
    if (!this.control.value) return true;
    var regEx = /^\d+(\.\d+)?$/;
    if (!regEx.test(this.control.value)) {
        alert('系统提示：\n\n' + this.name + '格式错误');
        return false;
    }
    else {
        return true;
    }
}
ValidateInput.prototype.validateNoneZero = function () {
    if (!this.control.value) return true;
    var value = this.control.value;
    if (value == '0' || value == '0.000') {
        alert('系统提示：\n\n' + this.name + '必须输入！');
        return false;
    }
    else {
        return true;
    }
}
ValidateInput.prototype.validateFormat = function () {
    if (!this.regular.test(this.control.value)) {
        alert('系统提示：\n\n' + this.name + '格式错误');
        return false;
    }
    else {
        return true;
    }
}