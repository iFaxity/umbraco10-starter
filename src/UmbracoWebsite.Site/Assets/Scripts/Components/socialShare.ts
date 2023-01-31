import { ancestorOrSelf, findElementById } from "../Utils";

interface SocialLink {
    url: string;
    title: string;
}

type SocialServiceType = 'facebook'|'twitter'|'linkedin'|'email'|'print';
type SocialServiceAction = (link: SocialLink) => string;

export function registerSocialShare(): void {
    const SERVICES: Record<SocialServiceType, SocialServiceAction> = {
        facebook(link) {
            return `http://www.facebook.com/sharer.php?u=${link.url}`;
        },
        twitter(link) {
            return `https://twitter.com/intent/tweet?url=${link.url}text=${link.title}`;
        },
        linkedin(link) {
            return `https://www.linkedin.com/shareArticle?mini=true&url=${link.url}&title=${link.title}`;
        },
        email(link) {
            return `mailto:?subject=${link.title}&body=${link.url}`;
        },
        print() {
            return 'print:';
        },
    };

    function openWindow(url: string, target: string, width: number, height: number): void {
        const win = window?.top ?? window;
        const top = win.outerHeight / 2 + win.screenY - height / 2;
        const left = win.outerWidth / 2 + win.screenX - width / 2;
        const features = `toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no,width=${width},height=${height},top=${top},left=${left}`;

        window.open(url, target, features);
    }

    function onClick(e: MouseEvent): void {
        const target = ancestorOrSelf<HTMLElement>(e.target as HTMLElement, '[data-social-share]');
        const service = target?.getAttribute('data-social-share') as SocialServiceType|null;
        const title = encodeURIComponent(document.title);
        const url = encodeURIComponent(window.location.href);

        if (service == null || !(service in SERVICES)) {
            throw new Error(`SocialService ${service} is invalid!`);
        }

        const action = SERVICES[service];
        const socialLink = action({
            url: url,
            title: title,
        });

        switch (service) {
            case 'print':
                window.print();
                break;

            case 'email':
                window.location.href = socialLink;
                break;

            default:
                openWindow(socialLink, title, 500, 300);
                break;
        }
    }

    // Initialize
    const $socialShare = findElementById('socialShare');

    $socialShare?.addEventListener('click', onClick, false);
}
