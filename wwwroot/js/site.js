window.confirmDelete = async function () {
    const result = await Swal.fire({
        title: "¿Estás seguro?",
        text: "¡Esta acción no se puede deshacer!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Sí, ¡eliminar!",
        cancelButtonText: "Cancelar"
    });
    return result.isConfirmed;
};

// Placeholder for initializeInactivityDetection
window.initializeInactivityDetection = function (objRef, timeoutInMinutes) {
    console.log("initializeInactivityDetection called with:", objRef, timeoutInMinutes);
    // Actual implementation would go here
};

// Placeholder for maximizeWindow
window.maximizeWindow = function () {
    console.log("maximizeWindow called");
    // Actual implementation would go here
};

// --- Scroll to Top Button Logic ---
let backToTopButtonObjRef = null;
let scrollListener = null;

window.initializeScrollToTop = function (objRef, buttonId) {
    backToTopButtonObjRef = objRef;

    if (scrollListener) {
        window.removeEventListener('scroll', scrollListener);
    }

    scrollListener = () => {
        if (window.scrollY > 200) { // Show button after scrolling 200px
            backToTopButtonObjRef.invokeMethodAsync('SetVisibility', true);
        } else {
            backToTopButtonObjRef.invokeMethodAsync('SetVisibility', false);
        }
    };

    window.addEventListener('scroll', scrollListener);
    // Initial check in case page loads already scrolled
    scrollListener();
};

window.disposeScrollToTop = function (objRef) {
    if (scrollListener) {
        window.removeEventListener('scroll', scrollListener);
        scrollListener = null;
    }
    if (backToTopButtonObjRef) {
        backToTopButtonObjRef.dispose();
        backToTopButtonObjRef = null;
    }
};

window.scrollToTop = function () {
    window.scrollTo({ top: 0, behavior: 'smooth' });
};
