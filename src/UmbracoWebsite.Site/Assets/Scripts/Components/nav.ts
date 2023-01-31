import { navFullscreen, toggleVisibility } from '../Utils';

export function registerNavMenu(): void {
  const SELECTORS = {
    nav: '#nav-menu',
    navToggle: '#nav-menu-toggle',
    navToggleExpand: '.menu-expand',
    navToggleCollapse: '.menu-collapse',
    subnav: '.accordion',
    subnavToggleIcons: '.accordion-expand, .accordion-collapse',
    subnavToggle: '.accordion-toggle',
  };

  const CLASSES = {
    navToggleActive: [],
  };

  const nav = document.querySelector<HTMLElement>(SELECTORS.nav);
  const navToggle = document.querySelector<HTMLElement>(SELECTORS.navToggle);

  function toggleMenu(btn: HTMLButtonElement, state?: boolean): void {
    state = state ?? btn.getAttribute('aria-expanded') != 'true';

    btn.setAttribute('aria-expanded', String(state));
    toggleVisibility(btn.querySelector(SELECTORS.navToggleExpand), !state);
    toggleVisibility(btn.querySelector(SELECTORS.navToggleCollapse), state);

    if (state) {
      btn.classList.add(...CLASSES.navToggleActive);
      
    } else {
      btn.classList.remove(...CLASSES.navToggleActive);
    }

    if (nav) {

      if (state === true) {
        toggleVisibility(document.querySelector('#quicksearch'), false);
        toggleVisibility(document.querySelector('.search-expand'), true);
        toggleVisibility(document.querySelector('.search-collapse'), false);
      }

      toggleVisibility(nav, state);
      navFullscreen(state);

    }
  }

  navToggle?.addEventListener('click', (e) => {
    if (e.currentTarget instanceof HTMLButtonElement) {
      toggleMenu(e.currentTarget);
    }
  }, false)
}
