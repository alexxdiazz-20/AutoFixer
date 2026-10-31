// Filas de servicios en Crear/Editar orden de servicio.
// - Clona la primera fila (ya trae las opciones del select desde el servidor)
// - Renumera Details[0], Details[1]... para que ASP.NET reciba todas las filas
// - Muestra subtotal por fila, cantidad de servicios y total estimado
$(function () {
    const $container = $("#service-details");
    if ($container.length === 0) return;

    const $template = $container.find(".service-detail-item").first().clone();
    const money = new Intl.NumberFormat("es-DO", { style: "currency", currency: "DOP" });

    function reindex() {
        $container.find(".service-detail-item").each(function (i) {
            $(this).find("[name^='Details[']").each(function () {
                this.name = this.name.replace(/Details\[\d+\]/, "Details[" + i + "]");
            });
        });
    }

    function updateSummary() {
        let total = 0;
        const $rows = $container.find(".service-detail-item");
        $rows.each(function () {
            const price = parseFloat($(this).find(".service-select option:selected").data("price")) || 0;
            const quantity = parseInt($(this).find(".service-quantity").val(), 10) || 0;
            const subtotal = price * quantity;
            total += subtotal;
            $(this).find(".service-subtotal").text(money.format(subtotal));
        });
        $("#service-count").text($rows.length);
        $("#service-total").text(money.format(total));
    }

    $("#add-service").on("click", function () {
        const $row = $template.clone();
        $row.find("select").prop("selectedIndex", 0);
        $row.find("input[type=number]").val(1);
        $row.find("input:not([type=number])").val("");
        $container.append($row);
        reindex();
        updateSummary();
        $row.find("select").trigger("focus");
    });

    $container.on("click", ".remove-service", function () {
        $(this).closest(".service-detail-item").remove();
        reindex();
        updateSummary();
    });

    $container.on("change input", ".service-select, .service-quantity", updateSummary);

    updateSummary();
});
