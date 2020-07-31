function set_ddl_date(year_start) {
    var now = new Date();

    //年(year_start~今年)
    for (var i = now.getFullYear(); i >= year_start; i--) {
        $('#ddl_year').
            append($("<option></option>").
                attr("value", i).
                text(i));
    }

    //月
    for (var i = 1; i <= 12; i++) {
        $('#ddl_month').
            append($("<option></option>").
                attr("value", i).
                text(i));
    }

    $('#ddl_year').change(onChang_date);
    $('#ddl_month').change(onChang_date);

    //年、月選單改變時
    function onChang_date() {
        if ($('#ddl_year').val() != -1 && $('#ddl_month').val() != -1) {

            var date_temp = new Date($('#ddl_year').val(), $('#ddl_month').val(), 0);

            //移除超過此月份的天數
            $("#ddl_day option").each(function () {
                if ($(this).val() != -1 && $(this).val() > date_temp.getDate()) $(this).remove();
            });

            //加入此月份的天數
            for (var i = 1; i <= date_temp.getDate(); i++) {
                if (!$("#ddl_day option[value='" + i + "']").length) {
                    $('#ddl_day').
                        append($("<option></option>").
                            attr("value", i).
                            text(i));
                }
            }
        } else {
            $("#ddl_day option:selected").removeAttr("selected");
        }
    }
}

//1911為開始年份
set_ddl_date(1990);