import { toggleVisibility } from '../Utils/dom';

export function registerLanguagePicker(): void {

  const SELECTORS = {
    languagePicker: "#languagepicker",
    languagePickerToggle: "[data-languagepicker]",
  }

  const languagePicker = document.querySelector<HTMLElement>(SELECTORS.languagePicker);
  const languagePickerToggles = document.querySelectorAll<HTMLElement>(SELECTORS.languagePickerToggle);

  function toggleLanguagePicker(btn: HTMLButtonElement, state?: boolean): void {
    state = state ?? btn.getAttribute('aria-expanded') != 'true';

    btn.setAttribute('aria-expanded', String(state));

    if (languagePicker) {
      toggleVisibility(languagePicker, state);
    }
  }

  languagePickerToggles.forEach(toggle => {
    toggle?.addEventListener('click', (e) => {
      if (e.currentTarget instanceof HTMLButtonElement) {
        toggleLanguagePicker(e.currentTarget);
      }
    }, false)
  });


}
