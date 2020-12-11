import { IStory, IStoryColumns, IStoryHistory } from '../../types/storyTypes';
import { IState } from '../store/state';

export function getColumns(state: IState): IStoryColumns[] {
    return state.stories.columns;
}

export const getStoriesForColumn = (columnId: string) => (state: IState): IStory[] => {
    return state.stories.columns.find((column) => column.key === columnId).value;
};

export const getAllStories = (state: IState): IStory[] => {
    return state.stories.columns.map((x) => x.value).reduce((acc, x) => acc.concat(x), []);
};

export function getSelectedStory(state: IState): IStory {
    return state.stories.selectedStory;
}

export function getStoryTitleTerm(state: IState): string {
    return state.stories.storyTitleTerm;
}

export function getWasStoryBlocked(state: IState): boolean {
    return state.stories.wasStoryBlocked;
}

export function getStoryHistory(state: IState): IStoryHistory[] {
    return state.stories.storyHistory;
}

export function getSearchResults(state: IState): IStory[] {
    return state.stories.searchResult;
}

export function getIsDragging(state: IState): boolean {
    return state.stories.isDragging;
}

export function getSortType(state: IState): string {
    return state.stories.sortType;
}
