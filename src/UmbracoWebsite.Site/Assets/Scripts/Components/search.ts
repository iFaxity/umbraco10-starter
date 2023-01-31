import { navFullscreen, toggleVisibility } from '../Utils';

export function registerQuickSearch(): void {

  const SELECTORS = {
    quickSearch: '#quicksearch',
    quickSearchToggle: '[data-quicksearch]',
    quickSearchToggleExpand: '.search-expand',
    quickSearchToggleCollapse: '.search-collapse',
  }

  const CLASSES = {
    quickSearchToggleActive: [],
  };

  const quickSearch = document.querySelector<HTMLElement>(SELECTORS.quickSearch);
  const quickSearchToggles = document.querySelectorAll<HTMLElement>(SELECTORS.quickSearchToggle);

  function toggleQuickSearch(btn: HTMLButtonElement, state?: boolean): void {
    state = state ?? btn.getAttribute('aria-expanded') != 'true';

    btn.setAttribute('aria-expanded', String(state));
    toggleVisibility(btn.querySelector(SELECTORS.quickSearchToggleExpand), !state);
    toggleVisibility(btn.querySelector(SELECTORS.quickSearchToggleCollapse), state);

    if (state) {
      btn.classList.add(...CLASSES.quickSearchToggleActive);
    } else {
      btn.classList.remove(...CLASSES.quickSearchToggleActive);
    }

    if (quickSearch) {
      
      toggleVisibility(quickSearch, state);
      navFullscreen(state);

      if (state === true) {
        toggleVisibility(document.querySelector('#nav-menu'), false);
        toggleVisibility(document.querySelector('.menu-expand'), true);
        toggleVisibility(document.querySelector('.menu-collapse'), false);

        document.querySelector<HTMLInputElement>('#search')?.focus();
      }

    }
  }

  quickSearchToggles.forEach(toggle => {
    toggle?.addEventListener('click', (e) => {
      if (e.currentTarget instanceof HTMLButtonElement) {
        toggleQuickSearch(e.currentTarget);
      }
    }, false)
  });
}
