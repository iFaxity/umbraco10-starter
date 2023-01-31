import Plyr from 'plyr';

/**
 * Creates a Plyr video element
 */
export function registerPlyr(): void {
  Plyr.setup('.plyr', {
    controls: [
      'play-large', // The large play button in the center
      'play', // Play/pause playback
      'progress', // The progress bar and scrubber for playback and buffering
      'mute', // Toggle mute
      'volume', // Volume control
      'fullscreen', // Toggle fullscreen
    ],
  });
  Plyr.setup('.plyr-inline', {
    autoplay: true,
    hideControls: true,
    clickToPlay: false,
    muted: true,
    controls: [],
  });
}

