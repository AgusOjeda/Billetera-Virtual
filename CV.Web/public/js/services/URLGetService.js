export const getParamFromURL = (param) => {
    let params = new URLSearchParams(location.search);
    return params.get(param);
}