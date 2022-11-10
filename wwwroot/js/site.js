
$(".editor").trumbowyg({
    lang: 'ko', //언어 : 한국어
    btnsDef: {
        image: {
            dropdown: ['insertImage', 'upload'],
            ico:'insertImage'
        }
    },
    btns: [
        ['viewHTML'],
        ['undo', 'redo'], // Only supported in Blink browsers
        ['formatting'],
        ['strong', 'em', 'del'],
        ['superscript', 'subscript'],
        ['link'],
        //['insertImage'],
        'image',
        ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
        ['unorderedList', 'orderedList'],
        ['horizontalRule'],
        ['removeformat'],
        ['fullscreen']
    ]
});
