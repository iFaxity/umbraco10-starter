

function navigate(el: Element): void {
  const link = el.getAttribute('data-link');
  const target = el.getAttribute('data-target');

  // Synthetic navigation, to allow for noreferrer and noopener
  if (link) {
    const a = document.createElement('a');

    a.href = link;

    // Thought: Maybe only set when the target link is outside of the sites domain?
    a.rel = 'noreferrer noopener';

    // External target, i.e open in another page?
    if (target) {
      a.target = target;
      
    }

    a.click();
  }
}

function registerEvents(el: HTMLElement): void {
  // If the "card" itself was clicked
  el.addEventListener('click', (e) => {
    const isTextSelected = !!window.getSelection()?.toString();

    if (!isTextSelected && e.currentTarget) {
      navigate(e.currentTarget as Element);
    }
  });

  el.addEventListener('keyup', (e: KeyboardEvent) => {
    if (e.key == 'Enter') {
      e.preventDefault();
      navigate(e.target as Element);
    }
  });
}

export function registerCards(): void {
  const cards = document.querySelectorAll<HTMLElement>('[data-link]');

  cards.forEach(card => registerEvents(card));
}

