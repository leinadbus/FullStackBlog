export interface AddBlogPost {
    Title: string;
    ShortDescription: string;
    Content: string;
    FeaturedImageUrl: string;
    UrlHandle: string;
    Author: string;
    PublishedDate: Date;
    IsVisible: boolean;
}