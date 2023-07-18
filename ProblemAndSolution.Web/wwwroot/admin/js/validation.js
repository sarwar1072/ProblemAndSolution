$.validator.addMethod("maxsize", function (value, element, params) {
    const fileSize = parseFloat(element.files[0].size);
    const requiredSize = parseFloat(params["size"]) * 1000;
    if (fileSize <= requiredSize) {
        return true;
    } else {
        return false;
    }
})

$.validator.unobtrusive.adapters.add('maxsize', ['size'], function (options) {
    options.rules['maxsize'] = options.params;
    options.messages['maxsize'] = options.message;
});

$.validator.addMethod("filetype", function (value, element, params) {
    const fileType = "." + value.split('.').pop();
    if (params["types"].includes(fileType)) {
        return true;
    } else {
        return false;
    }
})

$.validator.unobtrusive.adapters.add('filetype', ['types'], function (options) {
    options.rules['filetype'] = options.params;
    options.messages['filetype'] = options.message;
});