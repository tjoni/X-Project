function addFocusInPopup(popup, element, event) {
    // popup - modal id
    // element - element being focused
    // event - show modal event
    var $Modal = $(popup);
    var $ModalElement = $(element);

    $Modal.on(event, function () {
        document.activeElement.blur();
        $ModalElement.focus();
    });
};

addFocusInPopup("#example-modal", "#input", "shown.bs.modal");