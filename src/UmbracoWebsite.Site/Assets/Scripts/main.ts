
import '../Styles/style.css';

import { documentReady } from './Utils';
import { registerNavMenu } from './Components/nav';
import { registerQuickSearch } from './Components/search'
import { registerAccordions } from './Components/accordion';
import { registerLocationList } from './Components/locations';
import { registerLanguagePicker } from './Components/languagePicker';
import { registerCards } from './Components/card';
import { registerPlyr } from './Components/plyr';
import { registerSocialShare } from './Components/socialShare';

documentReady(() => {
  registerAccordions();
  registerNavMenu();
  registerQuickSearch();
  registerLocationList();
  registerLanguagePicker();
  registerCards();
  registerPlyr();
  registerSocialShare();
});
