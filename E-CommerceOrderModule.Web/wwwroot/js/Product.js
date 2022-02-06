$(document).ready(function () {
	$(document).on('change', '.btn-file :file', function () {
		var input = $(this),
			label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
		input.trigger('fileselect', [label]);
	});

	$('.btn-file :file').on('fileselect', function (event, label) {

		var input = $(this).parents('.input-group').find(':text'),
			log = label;

		if (input.length) {
			input.val(log);
		} else {
			if (log) alert(log);
		}

	});
	function readURL(input) {
		if (input.files && input.files[0]) {
			var reader = new FileReader();

			reader.onload = function (e) {
				$('#img-upload').attr('src', e.target.result);

			}

			reader.readAsDataURL(input.files[0]);
		}
	}

	$("#imgInp").change(function () {
		readURL(this);
	});

	var categoryValue = $("#CategoryName").attr("data-value");
	$("#CategoryName").val(categoryValue);
	
	var brandValue = $("#BrandName").attr("data-value");
	$("#BrandName").val(brandValue);
});

function ProductCreate() {
	window.location.href = "/Product/Create";
}

function ProductSave() {
	var Name = $("#Name").val();
	var ShortDescription = $("#ShortDescription").val();
	var Description = $("#Description").val();
	var CategoryName = $("#CategoryName").val();
	var BrandName = $("#BrandName").val();
	var SalePrice = $("#SalePrice").val();
	var Stock = $("#Stock").val();


	if (Name === null || Name === "") {
		$("#spanName").text("Ürün Adını Doldururunz");
	}
	else {
		$("#spanName").text("");
	}

	if (ShortDescription === null || ShortDescription === "") {
		$("#spanShortDescription").text("Kısa Açıklama Alanını Doldururunz");
	}
	else {
		$("#spanShortDescription").text("");
	}

	if (Description === null || Description === "") {
		$("#spanDescription").text("Açıklama Alanını Doldururunz");
	}
	else {
		$("#spanDescription").text("");
	}

	if (SalePrice === null || SalePrice === "") {
		$("#spanSalePrice").text("Fiyat Alanını Doldururunz");
	}
	else {
		$("#spanSalePrice").text("");
	}

	if (Stock === null || Stock === "") {
		$("#spanStock").text("Stok Alanını Doldururunz");
	}
	else {
		$("#spanStock").text("");
	}

	if ($("#spanName").text() === "" && $("#spanShortDescription").text() === "" && $("#spanDescription").text() === "" && $("#spanSalePrice").text() === "" && $("#spanStock").text() === "" ) {
		Lobibox.confirm({
			baseClass: 'animated-super-fast save-confirm',
			iconClass: false,
			title: '<span>Ürün Ekleme</span>',
			msg: '<span>Ürün Kaydedilsin mi?</span>',
			width: 350,
			buttons: {
				yes: { 'class': 'LBtn YesCheck dbtn btn-10', closeOnClick: false, text: 'EVET', closeOnClick: true },
				no: { 'class': 'LBtn NoTimes dbtn btn-11', closeOnClick: false, text: 'HAYIR', closeOnClick: true },
			},
			callback: function ($this, type) {
				if (type === 'yes') {
					var product = {
						Name: Name,
						ShortDescription: ShortDescription,
						Description: Description,
						CategoryName: CategoryName,
						BrandName: BrandName,
						SalePrice: SalePrice,
						Stock: Stock
					};

					$.post("/Product/Create", { product: product }, function (response) {
						if (response) {
							Lobibox.notify('success', {
								size: 'mini',
								msg: "İşlem Başarılı"
							});
							setTimeout(function () { window.location.href = "/Product/List"; }, 2000);

						} else {
							Lobibox.notify('error', {
								size: 'mini',
								msg: "İşlem Başarısız"
							});
						}
					});
				}
				else if (type === 'no') {
					Lobibox.notify('error', {
						size: 'mini',
						msg: 'Kaydetme İşlemi İptal Edildi.',
						width: 280
					});
				}
			}
		});
	}
	

}

function ProductUpdate() {
	var Name = $("#Name").val();
	var ShortDescription = $("#ShortDescription").val();
	var Description = $("#Description").val();
	var CategoryName = $("#CategoryName").val();
	var BrandName = $("#BrandName").val();
	var SalePrice = $("#SalePrice").val();
	var Stock = $("#Stock").val();

	if (Name === null || Name === "") {
		$("#spanName").text("Ürün Adını Doldururunz");
	}
	else {
		$("#spanName").text("");
	}

	if (ShortDescription === null || ShortDescription === "") {
		$("#spanShortDescription").text("Kısa Açıklama Alanını Doldururunz");
	}
	else {
		$("#spanShortDescription").text("");
	}

	if (Description === null || Description === "") {
		$("#spanDescription").text("Açıklama Alanını Doldururunz");
	}
	else {
		$("#spanDescription").text("");
	}

	if (SalePrice === null || SalePrice === "") {
		$("#spanSalePrice").text("Fiyat Alanını Doldururunz");
	}
	else {
		$("#spanSalePrice").text("");
	}

	if (Stock === null || Stock === "") {
		$("#spanStock").text("Stok Alanını Doldururunz");
	}
	else {
		$("#spanStock").text("");
	}

	if ($("#spanName").text() === "" && $("#spanShortDescription").text() === "" && $("#spanDescription").text() === "" && $("#spanSalePrice").text() === "" && $("#spanStock").text() === "") {
		Lobibox.confirm({
			baseClass: 'animated-super-fast save-confirm',
			iconClass: false,
			title: '<span>Ürün Güncelleme</span>',
			msg: '<span>Ürün Güncellensin mi?</span>',
			width: 350,
			buttons: {
				yes: { 'class': 'LBtn YesCheck dbtn btn-10', closeOnClick: false, text: 'EVET', closeOnClick: true },
				no: { 'class': 'LBtn NoTimes dbtn btn-11', closeOnClick: false, text: 'HAYIR', closeOnClick: true },
			},
			callback: function ($this, type) {
				if (type === 'yes') {
					var product = {
						Name: Name,
						ShortDescription: ShortDescription,
						Description: Description,
						CategoryName: CategoryName,
						BrandName: BrandName,
						SalePrice: SalePrice,
						Stock: Stock,
						Id: $("#Id").val(),
						Status: $("#Status").val(),
						ProductId: $("#ProductId").val(),
						UploadDate: $("#UploadDate").val(),
						UpdateDate: $("#UpdateDate").val(),
						KDV: $("#KDV").val(),
						Image: $("#Image").val(),
						MarketPrice: $("#MarketPrice").val()
					};

					$.post("/Product/Edit", { product: product }, function (response) {
						if (response) {
							Lobibox.notify('success', {
								size: 'mini',
								msg: "İşlem Başarılı"
							});
							setTimeout(function () { window.location.href = "/Product/List"; }, 2000);

						} else {
							Lobibox.notify('error', {
								size: 'mini',
								msg: "İşlem Başarısız"
							});
						}
					});
				}
				else if (type === 'no') {
					Lobibox.notify('error', {
						size: 'mini',
						msg: 'Kaydetme İşlemi İptal Edildi.',
						width: 280
					});
				}
			}
		});
	}
}

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

function ProductDelete(e) {
	Lobibox.confirm({
		baseClass: 'animated-super-fast save-confirm',
		iconClass: false,
		title: '<span>Ürün Silme</span>',
		msg: '<span>Emin misiniz?</span>',
		width: 350,
		buttons: {
			yes: { 'class': 'LBtn YesCheck dbtn btn-10', closeOnClick: false, text: 'EVET', closeOnClick: true },
			no: { 'class': 'LBtn NoTimes dbtn btn-11', closeOnClick: false, text: 'HAYIR', closeOnClick: true },
		},
		callback: function ($this, type) {
			if (type === 'yes') {
				var id = e;
				$.post("/Product/ProductDelete", { id: id }, function (res) {
					if (res) {
						Lobibox.notify('success', {
							size: 'mini',
							msg: "Silme İşlemi Başarılı"
						});
					}
					else {
						Lobibox.notify('error', {
							size: 'mini',
							msg: 'İşlem Başarısız',
							width: 280
						});
					}
				});
			
				setTimeout(function () { window.location.href = "/Product/List"; }, 2000);
			}
			else if (type === 'no') {
				Lobibox.notify('error', {
					size: 'mini',
					msg: 'Silme İşlemi İptal Edildi.',
					width: 280
				});
			}
		}
	});
}