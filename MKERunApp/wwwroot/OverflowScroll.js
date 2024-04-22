window.observeElementHeight = function (elementId, dotNetReference) {
    const element = document.getElementById(elementId);
    const resizeObserver = new ResizeObserver(entries => {
        for (let entry of entries) {
            const newHeight = entry.contentRect.height;
            dotNetReference.invokeMethodAsync('OnElementHeightChanged', newHeight);
        }
    });
    resizeObserver.observe(element);
};
