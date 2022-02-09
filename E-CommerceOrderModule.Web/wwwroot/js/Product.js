
function BasketAdd(e) {
	var id = e;
	$.post("/Product/BasketAdd", { id: id }, function (res) {
		if (res) {
			Lobibox.notify('success', {
				size: 'mini',
				msg: "Ürün Sepete Eklendi."
			});
			setTimeout(function () { window.location.href = "/Product/List"; }, 2000);
		}
		else {
			Lobibox.notify('error', {
				size: 'mini',
				msg: "İşlem Başarısız"
			});
		}
	});
}
