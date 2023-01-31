


function onFilter(locations: HTMLElement[], locationsHolder: HTMLElement, filterInput: HTMLInputElement, selectInput: HTMLSelectElement) {
  var currentFilter = filterInput.value;
  var currentSelected = selectInput.value;
  locationsHolder.innerHTML = "";

  // If no filter is set and no landscape is selected then just show the original list of locations
  if (!currentFilter && currentSelected === 'all') {
    locations.forEach(location => locationsHolder.appendChild(location));
    return;
  }

  // See if the filter and selected landscape is a match
  var filteredLocations = locations.filter(item => {
    var filterMatch = item.dataset?.name?.toLowerCase().includes(currentFilter.toLowerCase());
    var landscapeMatch = currentSelected === 'all' ? true : item.dataset?.landscape?.toLowerCase() === currentSelected.toLowerCase();

    return filterMatch && landscapeMatch;
  });

  // Display the filtered locations
  filteredLocations.forEach(location => locationsHolder.appendChild(location));
}

export function registerLocationList() {
  // There might be several of the same module on the page, handle each one seperatly
  var locationModules = document.querySelectorAll<HTMLElement>('[data-locations]');

  // Go through and setup each module
  locationModules.forEach(el => {

    // Default values / State


    // 1. Retrieve the list of locations
    var locationsHolder = el.querySelector<HTMLElement>('[data-locations-list]');
    if (locationsHolder) {

      // 2. Retrieve the actual locations (in our case each location is a details/summary setup)
      var locations = Array.from(locationsHolder.querySelectorAll<HTMLElement>('details'));

      // 3. Setup the filtering methods (text-filter by name and select by landscape)
      var filterInput = el.querySelector<HTMLInputElement>('[data-locations-filter]');
      var selectInput = el.querySelector<HTMLSelectElement>('[data-locations-landscape]');

      filterInput && filterInput.addEventListener('keyup', () => onFilter(locations, locationsHolder!, filterInput!, selectInput!));
      selectInput && selectInput.addEventListener('change', () => onFilter(locations, locationsHolder!, filterInput!, selectInput!));
    }
  });
}
