export const loadIntoSS = (den, val) => {
    sessionStorage.setItem(den, JSON.stringify(val))
}

export const readFromSS = (den) => {
    return JSON.parse(sessionStorage.getItem(den))
}

export const cleanSS = () => {
    sessionStorage.clear();
}

export const removeFromSS = (den) => {
    sessionStorage.removeItem(den);
}