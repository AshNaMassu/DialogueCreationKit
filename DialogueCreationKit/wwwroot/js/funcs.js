window.downloadFile = (fileName, byteArray) => {
    // Создание нового Blob из байтового массива
    const blob = new Blob([byteArray], { type: "application/json" });

    // Создание ссылки на Blob
    const url = URL.createObjectURL(blob);

    // Создание ссылки на скачивание файла
    const link = document.createElement("a");
    link.href = url;
    link.download = fileName;

    // Добавление ссылки на страницу и эмуляция клика для скачивания файла
    document.body.appendChild(link);
    link.click();

    // Очистка ресурсов
    document.body.removeChild(link);
    URL.revokeObjectURL(url);
};