export interface IStory {
    storyId: string;
    title: string;
    description: string;
    notes: string;
    columnType: string;
    estimate: number;
    isDefect: boolean;
    isReady: boolean;
    isBlocked: boolean;
    blockReason: string;
    creationDate: string;
    userId: string;
    sprintId: string;
    priority: Priority;
}

export interface ISelectedItem {
    key: string;
    value: string;
}

export interface IStoryDragAndDrop {
    columnTypeOrigin: string;
    columnTypeDestination: string;
    storyId: string;
}

export interface IStoryColumns {
    key: string;
    value: IStory[];
}

export enum Priority {
    LOW = 'Low',
    MEDIUM = 'Medium',
    HIGH = 'High',
}

export const SortFields = {
    PRIORITY: 'Priority',
    NAME: 'Name',
    ESTIMATE: 'Estimate',
    CREATION_DATE: 'Creation Date',
};
