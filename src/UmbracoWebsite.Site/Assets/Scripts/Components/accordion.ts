import { toggleVisibility, siblings, children } from '../Utils';

const SELECTORS = {
  accordion: '.accordion',
  toggleIcons: '.accordion-expand, .accordion-collapse',
  toggle: '.accordion-toggle',
  button: '.accordion-button',
  expand: '.accordion-expand',
  collapse: '.accordion-collapse',
};

const CLASSES = {
  btnToggle: []
}

const DATA_OPEN_ACTIVE = 'open-active';
const DATA_COLLAPSE = 'collapse';
const PRIMARY_CLASS = 'accordion-primary';

function toggleAccordion(accordion: HTMLElement, btn: HTMLElement, state?: boolean): void {
  const expanded = btn.getAttribute('aria-expanded') == 'true';

  if (typeof state !== 'boolean') {
    state = !expanded;
  } else if (expanded === state) {
    return;
  }

  const target = btn.dataset.target;
  const subMenu = accordion.querySelector('#' + target);

  if (accordion.classList.contains(PRIMARY_CLASS)) {
    btn.classList.add(...CLASSES.btnToggle);
  } else {
    btn.classList.remove(...CLASSES.btnToggle);
  }

  toggleVisibility(btn.querySelector(SELECTORS.collapse), state);
  toggleVisibility(btn.querySelector(SELECTORS.expand), !state);

  btn.setAttribute('aria-expanded', String(state));
  subMenu?.classList.toggle('hidden');
}

function onClick(e: MouseEvent | KeyboardEvent): void {
  e.stopPropagation();

  if (!(e.currentTarget instanceof HTMLElement)) {
    return;
  }

  const btn = e.currentTarget;
  const accordion = btn.closest<HTMLElement>(SELECTORS.accordion);
  const expanded = btn.getAttribute('aria-expanded') == 'true';
  const collapse = accordion?.dataset[DATA_COLLAPSE] ?? true;

  toggleAccordion(accordion!, btn, !expanded);

  if (!expanded && collapse) {
    siblings(accordion!, SELECTORS.accordion)?.forEach(el => {
      const btn = children(el, '.accordion-header')[0]?.querySelector<HTMLElement>(SELECTORS.toggle);
      if (btn && btn instanceof HTMLButtonElement) {
        toggleAccordion(el, btn, false);
      }
    });
  }
}

export function registerAccordions(): void {
  var accordions = document.querySelectorAll<HTMLElement>(SELECTORS.accordion);

  accordions.forEach(el => {
    const toggleButton = el.querySelector<HTMLElement>(SELECTORS.toggle);
    if (toggleButton) {
      toggleButton.addEventListener('click', e => {
        onClick(e);
        return e.preventDefault();
      }, false);

      toggleButton.addEventListener('keydown', e => {
        if (e.key == ' ' || e.key == 'Spacebar') {
          onClick(e);
          return e.preventDefault();
        }
      }, false);
    }
  });

  // Open active accordions on load
  const activeAccordions: HTMLElement[] = Array.prototype.filter.call(accordions, function (accordion: HTMLElement) {
    return accordion.classList.contains('active');
  });

  activeAccordions.forEach(accordion => {
     accordion.querySelector<HTMLElement>(SELECTORS.toggle)?.click();
  });
}
