//async function openFolderDialog() {
//    return new Promise((resolve, reject) => {
//        const input = document.createElement('input');
//        input.type = 'file';
//        input.webkitdirectory = true;
//        input.directory = true;

//        input.onchange = (event) => {
//            const files = event.target.files;
//            if (files.length > 0) {
//                const path = files[0].webkitRelativePath.split('/')[0];
//                resolve(path);
//            } else {
//                resolve('');
//            }
//        };

//        input.onerror = (error) => reject(error);
//        input.click();
//    });
//}

async function openFolderDialog() {
    try {
        // Use File System Access API to select directory
        const dirHandle = await window.showDirectoryPicker();
        const fullPath = await getFullPath(dirHandle);
        return fullPath;
    } catch (err) {
        console.error('Error accessing directory:', err);
        return '';
    }
}

async function getFullPath(dirHandle) {
    // Note: Actual full path access is limited by browser security
    // This will return the directory name as full path isn't directly accessible
    return dirHandle.name;
}