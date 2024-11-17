using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitUpAppBackend.Infrastructure.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetParameters_WorkoutSets_WorkoutSetId",
                table: "SetParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Workouts_WorkoutId",
                table: "WorkoutExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSets_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutSets");

            migrationBuilder.AddForeignKey(
                name: "FK_SetParameters_WorkoutSets_WorkoutSetId",
                table: "SetParameters",
                column: "WorkoutSetId",
                principalTable: "WorkoutSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Workouts_WorkoutId",
                table: "WorkoutExercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSets_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutSets",
                column: "WorkoutExerciseId",
                principalTable: "WorkoutExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetParameters_WorkoutSets_WorkoutSetId",
                table: "SetParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Workouts_WorkoutId",
                table: "WorkoutExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSets_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutSets");

            migrationBuilder.AddForeignKey(
                name: "FK_SetParameters_WorkoutSets_WorkoutSetId",
                table: "SetParameters",
                column: "WorkoutSetId",
                principalTable: "WorkoutSets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Workouts_WorkoutId",
                table: "WorkoutExercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSets_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutSets",
                column: "WorkoutExerciseId",
                principalTable: "WorkoutExercises",
                principalColumn: "Id");
        }
    }
}
