import { IStory } from '../types/storyTypes';

export const debouncePeriod: number = 1000;

export enum StoryActions {
    CREATE = 'Create',
    UPDATE = 'Update',
    REMOVE = 'REMOVE',
}

export enum storyFields {
    blockReason = 'blockReason',
    columnType = 'columnType',
    creationDate = 'creationDate',
    description = 'description',
    estimate = 'estimate',
    isBlocked = 'isBlocked',
    isDefect = 'isDefect',
    isReady = 'isReady',
    notes = 'notes',
    priority = 'priority',
    sprintId = 'sprintId',
    storyId = 'storyId',
    title = 'title',
    userId = 'userId',
}

export const initialStory: IStory = {
    [storyFields.blockReason]: '',
    [storyFields.columnType]: '',
    [storyFields.creationDate]: '',
    [storyFields.description]: '',
    [storyFields.estimate]: 0,
    [storyFields.isBlocked]: false,
    [storyFields.isDefect]: false,
    [storyFields.isReady]: false,
    [storyFields.notes]: '',
    [storyFields.priority]: undefined,
    [storyFields.sprintId]: '',
    [storyFields.storyId]: '',
    [storyFields.title]: '',
    [storyFields.userId]: '',
};
