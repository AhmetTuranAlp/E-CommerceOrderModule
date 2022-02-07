
function CompleteShopping(e) {
	Lobibox.confirm({
		baseClass: 'animated-super-fast save-confirm',
		iconClass: false,
		title: '<span>Alışveriş Sepeti</span>',
		msg: '<span>Alışveriş Tamamlansın mı?</span>',
		width: 350,
		buttons: {
			yes: { 'class': 'LBtn YesCheck dbtn btn-10', closeOnClick: false, text: 'EVET', closeOnClick: true },
			no: { 'class': 'LBtn NoTimes dbtn btn-11', closeOnClick: false, text: 'HAYIR', closeOnClick: true },
		},
		callback: function ($this, type) {
			if (type === 'yes') {
				$.post("/Basket/CompleteShopping", function (res) {
					if (res) {
						Lobibox.notify('success', {
							size: 'mini',
							msg: "İşlem Başarılı"
						});
						setTimeout(function () { window.location.href = "/Product/List"; }, 2000);
					}
					else {
						Lobibox.notify('error', {
							size: 'mini',
							msg: 'İşlem Başarısız',
							width: 280
						});
					}
				});
			}
			else if (type === 'no') {
				Lobibox.notify('error', {
					size: 'mini',
					msg: 'İşlem İptal Edildi.',
					width: 280
				});
			}
		}
	});
}





