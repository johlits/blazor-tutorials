window.math = {
    square: function (a) {
        return a * a;
    },
    logBinaryAsync: function (dec) {
        DotNet.invokeMethodAsync('BlazorWasmApp',
            'ReturnBinaryAsync', dec)
            .then(data => {
                console.log(data);
            });
    }
}