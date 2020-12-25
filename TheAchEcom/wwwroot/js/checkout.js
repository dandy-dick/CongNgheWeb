const paypal_helper = {
    createOrderRequest: function (callBack) {
        $.post(
            "/checkout/paypal/create",
            null,
            (res) => callBack(res)
        );
    },
};

paypal_helper.createOrderRequest((res) => {
    var orderDetail = res.orderDetail;
    paypal.Buttons({
        env: 'sandbox',
        createOrder: function (data, actions) {
            return actions.order.create(orderDetail);
        },
        onApprove: function (data, actions) {
            var result = actions.order.capture().then(function (details) {
                window.location.replace('/CheckOut/Complete');
            });
            return result;
        },
    }).render('#paypal_checkout');
});

const nganluong_helper = {
    createOrderRequest: function (callBack) {
        // request order detail
        $.post(
            "/checkout/nganluong/create",
            null,
            (res) => callBack(res)
        );
    }
}

function requestNganLuongCheckOut() {
    nganluong_helper.createOrderRequest((res) => {
        var orderDetail = res.orderDetail;
        var requestUrl = "https://sandbox.nganluong.vn:8088/nl35/checkout.php?";

        requestUrl += 'merchant_site_code=' + orderDetail.merchant_site_code+ "&";
        requestUrl += 'return_url=' + orderDetail.return_url+ "&";
        requestUrl += 'receiver=' + orderDetail.receiver+ "&";
        requestUrl += 'transaction_info=' + orderDetail.transaction_info+ "&";
        requestUrl += 'order_code=' + orderDetail.order_code+ "&";
        requestUrl += 'price=' + orderDetail.price+ "&";
        requestUrl += 'currency=' + orderDetail.currency+ "&";
        requestUrl += 'quantity=' + orderDetail.quantity+ "&";
        requestUrl += 'tax=' + orderDetail.tax+ "&";
        requestUrl += 'discount=' + orderDetail.discount+ "&";
        requestUrl += 'fee_cal=' + orderDetail.fee_cal+ "&";
        requestUrl += 'fee_shipping=' + orderDetail.fee_shipping+ "&";
        requestUrl += "order_description=" + orderDetail.order_description + "&";
        requestUrl += "buyer_info=" + orderDetail.buyer_info + "&";
        requestUrl += "affiliate_code=" + orderDetail.affiliate_code + "&";
        requestUrl += "lang=" + orderDetail.lang + "&";
        requestUrl += "secure_code=" + orderDetail.secure_code + "&";
        requestUrl += "cancel_url=" + orderDetail.cancel_url;

        debugger
        window.location.replace(requestUrl.substr(0, requestUrl.length - 1));
    });
}

function selectPaymentMethod(name) {
    if (name == 'paypal') {
        $('#nganluong_method').removeClass('active');
        $('#nganluong_checkout').removeClass('active');

        $('#paypal_method').addClass('active');
        $('#paypal_checkout').addClass('active');
    }
    else {
        $('#paypal_method').removeClass('active');
        $('#paypal_checkout').removeClass('active');

        $('#nganluong_method').addClass('active');
        $('#nganluong_checkout').addClass('active');
    }
}