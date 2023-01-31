/*
 * Helper function for toggling hidden class
 */
export function toggleVisibility(el: HTMLElement | null, override: boolean | null = null): void {
  if (!el)
    return;

  const className = 'hidden';

  if (typeof override != 'undefined') {
    el.classList.toggle(className, !override);
  } else {
    el.classList.toggle(className);
  }
}

/*
 * Navigation hack, borrowed from bootstrap
 */
export function navFullscreen(state: boolean): void {
  document.body.classList.toggle('nav-open', state);
}

export function siblings<T extends HTMLElement>(element: T, selector?: string): T[] {
  if (element.parentElement === null) return [];

  return Array.prototype.filter.call(element.parentElement.children, function (child: T) {

    if (selector && !child.classList.contains(selector)) {
      return false;
    }

    return child !== element;
  });
}

export function children<T extends HTMLElement>(element: T, selector: string): T[] {
  return Array.prototype.filter.call(element.children, function (child: T) {
    return child.classList.contains(selector) || child.id === selector;
  });
}

export function ancestorOrSelf<T extends Element>(element: Element, selector: string): T|null {
  let node = element;

  while (node.parentElement != null) {
    if (node.matches(selector)) {
      return node as T;
    }

    node = node.parentElement;
  }

  return null;
}

export function findElementById<T extends Element = HTMLElement>(id: string | null): T | null {
  if (!id?.length) {
    return null;
  }

  return document.getElementById(id) as T|null;
}


export function documentReady(callback: () => void) {
  // @ts-ignore
  if (document.readyState == 'complete' || document.readyState == 'loaded') {
    callback();
  } else {
    document.addEventListener('DOMContentLoaded', callback);
  }
}
